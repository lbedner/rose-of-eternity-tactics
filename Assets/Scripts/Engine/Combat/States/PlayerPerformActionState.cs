﻿using UnityEngine;
using System.Collections;

public class PlayerPerformActionState : PerformActionState {

	public override void Enter() {
		print ("PlayerPerformActionState.Enter");
		base.Enter ();
		Init ();
	}

	/// <summary>
	/// Init this instance.
	/// </summary>
	private void Init() {
		controller.ConfirmationSource.PlayOneShot (controller.ConfirmationSource.clip);
		controller.ClearActionTargets ();
		controller.Head2HeadPanel.gameObject.SetActive (false);
		controller.Head2HeadPanel.ClearPanels ();
		controller.ShowCursorAndTileSelector (false);
		controller.SelectionIndicator.ClearIndicators ();
		StartCoroutine (PerformAbilityAction (controller.HighlightedUnit));
	}
}