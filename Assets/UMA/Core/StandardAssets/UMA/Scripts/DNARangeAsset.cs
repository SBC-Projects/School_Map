﻿using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

namespace UMA
{
	/// <summary>
	/// Specifies the valid range of DNA values for a particular DNA converter and race.
	/// </summary>
	/// <remarks>
	/// Some DNA converters (for example the humanoid converters) don't actually produce
	/// valid results for the full range of DNA values. In this way the same converter
	/// can be used for multiple races, e.g. humans, elves, giants, and halflings can
	/// all be generated by the same HumanDNAConverterBehaviour. The DNA range asset
	/// is a way of specifying the values which are actually valid for a race.
	/// </remarks>
	[System.Serializable]
	public class DNARangeAsset : ScriptableObject
	{
		/// <summary>
		/// The DNA converter for which the ranges apply.
		/// </summary>
		public DnaConverterBehaviour dnaConverter;

		/// <summary>
		/// The mean (average) value for each DNA entry.
		/// </summary>
		public float[] means;
		/// <summary>
		/// The standard deviation for each DNA entry.
		/// </summary>
		/// <remarks>
		/// Used for Gaussian random values 99.7% of values will be within
		/// three standard deviations above or below the mean.
		/// </remarks>
		public float[] deviations;
		/// <summary>
		/// The spread above and below means for uniform ranges.
		/// </summary>
		public float[] spreads;

		private float[] values;

		public bool ContainsDNARange(int index, string name)
		{
			if (dnaConverter == null)
				return false;

			if (dnaConverter.DNAType == typeof(DynamicUMADna)) {
				if (((DynamicDNAConverterBehaviourBase)dnaConverter).dnaAsset.Names.Length > index) {
					if (Regex.Replace (((DynamicDNAConverterBehaviourBase)dnaConverter).dnaAsset.Names [index], "( )+", "") == Regex.Replace (name, "( )+", ""))
						return true;
				}
			}
			return false;
		}

		public bool ValueInRange(int index, float value)
		{
			float rangeMin = means[index] - spreads[index];
			float rangeMax = means[index] + spreads[index];
			if (value < rangeMin || value > rangeMax)
				return false;
			return true;
		}

		/// <summary>
		/// Uniformly randomizes each value in the DNA.
		/// </summary>
		/// <param name="data">UMA data.</param>
		public void RandomizeDNA(UMAData data)
		{
			if (dnaConverter == null)
				return;
			
			UMADnaBase dna = data.GetDna(dnaConverter.DNATypeHash);
			if (dna == null)
				return;

			int entryCount = dna.Count;
			if (means.Length != entryCount)
			{
				Debug.LogWarning("Range settings out of sync with DNA, cannot apply!");
				return;
			}

			if ((values == null) || (values.Length != entryCount))
				values = new float[entryCount];

			for (int i = 0; i < entryCount; i++)
			{
				values[i] = means[i] + (Random.value - 0.5f) * spreads[i];
			}

			dna.Values = values;
		}

		/// <summary>
		/// Randomizes each value in the DNA using a Gaussian distribution.
		/// </summary>
		/// <param name="data">UMA data.</param>
		public void RandomizeDNAGaussian(UMAData data)
		{
			if (dnaConverter == null)
				return;

			UMADnaBase dna = data.GetDna(dnaConverter.DNATypeHash);
			if (dna == null)
				return;
			
			int entryCount = dna.Count;
			if (means.Length != entryCount)
			{
				Debug.LogWarning("Range settings out of sync with DNA, cannot apply!");
				return;
			}
			
			if (values == null)
				values = new float[entryCount];
			
			for (int i = 0; i < entryCount; i++)
			{
				values[i] = UMAUtils.GaussianRandom(means[i], deviations[i]);
			}
			
			dna.Values = values;
		}
	}
}
