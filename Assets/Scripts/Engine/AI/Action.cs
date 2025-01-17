﻿using UnityEngine;
using System.Collections.Generic;

public class Action {

	public enum ActionType {
		ABILITY,
		ITEM,
	}

	public List<Unit> Targets { get; set; }
	public Vector3 TargetTile { get; set; }
	public Pathfinder Pathfinder { get; set; }
	public Ability Ability { get; set; }
	public Item Item { get; set; }
	public List<int> DamageToTargets { get; set; }
	public ActionType Type { get; private set; }

	private Dictionary<Unit, int> _damageByUnit = new Dictionary<Unit, int>();
	private Dictionary<Unit, List<Effect>> _effectsByUnit = new Dictionary<Unit, List<Effect>>();

	/// <summary>
	/// Initializes a new instance of the <see cref="Action"/> class.
	/// </summary>
	/// <param name="type">Type.</param>
	public Action(ActionType type ) {
		Type = type;
		ClearTargets ();
	}

	/// <summary>
	/// Clears the targets.
	/// </summary>
	public void ClearTargets() {
		Targets = new List<Unit> ();
		DamageToTargets = new List<int> ();
	}

	/// <summary>
	/// Determines whether this instance is self only.
	/// </summary>
	/// <returns><c>true</c> if this instance is self only; otherwise, <c>false</c>.</returns>
	public bool IsSelfOnly() {
		if (Type == ActionType.ABILITY)
			return Ability.TargetType == Ability.TargetTypeEnum.SELF;
		else if (Type == ActionType.ITEM)
			return true;
		return false;
	}

	/// <summary>
	/// Gets the range.
	/// </summary>
	/// <returns>The range.</returns>
	/// <param name="unit">Unit.</param>
	public int GetRange(Unit unit) {
		if (Type == ActionType.ABILITY && Ability.Id != AbilityConstants.ATTACK)
			return Ability.GetRange ();
		else if (Type == ActionType.ABILITY && Ability.Id == AbilityConstants.ATTACK)
			return unit.GetWeaponRange ();
		else if (Type == ActionType.ITEM)
			return (int)Item.GetAttributeCollection ().Get (AttributeEnums.AttributeType.RANGE).CurrentValue;
		else
			return 0;
	}

	/// <summary>
	/// Gets the AOE range.
	/// </summary>
	/// <returns>The AOE range.</returns>
	/// <param name="unit">Unit.</param>
	public int GetAOERange(Unit unit) {
		if (Type == ActionType.ABILITY && Ability.Id != AbilityConstants.ATTACK)
			return Ability.GetAOERange ();
		else if (Type == ActionType.ABILITY && Ability.Id == AbilityConstants.ATTACK)
			return 0;
		else if (Type == ActionType.ITEM)
			return 0;
		else
			return 0;
	}

	/// <summary>
	/// Adds the damage by unit.
	/// </summary>
	/// <param name="unit">Unit.</param>
	/// <param name="damage">Damage.</param>
	public void AddDamageByUnit(Unit unit, int damage) {
		_damageByUnit [unit] = damage;
	}

	/// <summary>
	/// Gets the damage by unit.
	/// </summary>
	/// <returns>The damage by unit.</returns>
	public Dictionary<Unit, int> GetDamageByUnit() {
		return _damageByUnit;
	}

	/// <summary>
	/// Adds the effects by unit.
	/// </summary>
	/// <param name="unit">Unit.</param>
	/// <param name="effects">Effects.</param>
	public void AddEffectsByUnit(Unit unit, List<Effect> effects) {
		_effectsByUnit [unit] = effects;
	}

	/// <summary>
	/// Gets the effects by unit.
	/// </summary>
	/// <returns>The effects by unit.</returns>
	public Dictionary<Unit, List<Effect>> GetEffectsByUnit() {
		return _effectsByUnit;
	}

	/// <summary>
	/// Returns a <see cref="System.String"/> that represents the current <see cref="Action"/>.
	/// </summary>
	/// <returns>A <see cref="System.String"/> that represents the current <see cref="Action"/>.</returns>
	public override string ToString ()
	{
		return string.Format ("[Action: Targets={0}, TargetTile={1}, Pathfinder={2}, Ability={3}]", Targets, TargetTile, Pathfinder, Ability);
	}
}