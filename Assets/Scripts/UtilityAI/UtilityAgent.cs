﻿using UnityEngine;
using System.Collections.Generic;

public class UtilityAgent : MonoBehaviour {
	protected List<AIStrategy> strategies = new List<AIStrategy>();
	protected AIStrategy currentStrategy;

	#region Strategy Bookkeeping
	protected S AddStrategy<S>(System.Action<AIStrategy> finishedHandler)
		where S : AIStrategy
	{
		var s = GetComponent<S> ();
		strategies.Add (s);
		s.FinishedHandler = finishedHandler;
		return s;
	}

	protected S AddStrategy<S>()
		where S : AIStrategy
	{
		var s = GetComponent<S> ();
		if (s == null) {
			s = gameObject.AddComponent <S>();
		}
		strategies.Add (s);
		s.FinishedHandler = OnStrategyFinished;
		return s;
	}

	public AIStrategy CurrentStrategy {
		get {
			return currentStrategy;
		}
		private set {
			if (currentStrategy != value) {
				//				if (value != null) {
				//					print ("New behavior: " + value.GetType().Name);
				//				}

				// behavior has changed: disable all behaviors and enable new behavior
				strategies.ForEach (b => b.enabled = false);
				value.enabled = true;
				currentStrategy = value;
			}
		}
	}

	AIStrategy GetOrCreateStrategyForAction(AIAction action) {
		var type = StrategyManager.GetStrategyTypeForAction (action);

		if (type == null) {
			print ("No strategy for action: " + action.GetType().Name);
			return null;
		}
		var comp = GetComponent (type);
		if (comp == null) {
			comp = gameObject.AddComponent (type);
		}
		return comp as AIStrategy;
	}
	#endregion

	protected virtual void Start() {
	}

	public AIAction PickBestAction() {
		//var action = ;
		return null;
	}

	protected virtual void Update () {
		var nextAction = PickBestAction();
		if (nextAction != null) {
			// change action
			StartStrategy (nextAction);
		} else {
			// keep doing what we are already doing
		}
	}

	protected virtual void StartStrategy(AIAction action) {
		var strategy = GetOrCreateStrategyForAction(action);

		if (currentStrategy != null && currentStrategy != strategy) {
			currentStrategy.StopStrategy ();
		}
		CurrentStrategy = strategy;
		strategy.StartStrategy (action);
	}

	protected virtual void OnStrategyFinished(AIStrategy strategy) {
		//CurrentStrategy = DefaultStrategy;
	}
}
