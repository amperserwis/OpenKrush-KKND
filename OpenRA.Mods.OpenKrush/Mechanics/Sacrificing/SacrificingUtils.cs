#region Copyright & License Information

/*
 * Copyright 2007-2022 The OpenKrush Developers (see AUTHORS)
 * This file is part of OpenKrush, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

#endregion

namespace OpenRA.Mods.OpenKrush.Mechanics.Sacrificing;

using OpenRA.Traits;
using Traits;

public static class SacrificingUtils
{
	public static bool CanEnter(Actor source, Actor target, out bool blocked)
	{
		blocked = false;

		if (target.IsDead || target.Disposed || !target.IsInWorld)
			return false;

		var trait = target.TraitOrDefault<Sacrificer>();

		if (trait == null)
			return false;

		if (!trait.IsTraitDisabled && source.Owner.RelationshipWith(target.Owner) == PlayerRelationship.Ally)
			return true;

		blocked = true;

		return false;
	}
}
