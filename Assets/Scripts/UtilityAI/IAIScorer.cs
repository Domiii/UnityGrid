using UnityEngine;
using System.Collections;

public interface IAIScorer {
	float ComputeScore(UtilityAgent agent);
}
