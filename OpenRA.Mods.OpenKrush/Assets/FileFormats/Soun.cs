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

namespace OpenRA.Mods.OpenKrush.Assets.FileFormats;

public class Soun : ISoundFormat
{
	private readonly byte[] data;

	public int Channels { get; } = 1;
	public int SampleBits { get; }
	public int SampleRate { get; }
	public float LengthInSeconds => (float)this.data.Length / (this.Channels * this.SampleRate * this.SampleBits);

	public Soun(Stream stream)
	{
		var size = stream.ReadInt32();
		this.SampleRate = stream.ReadInt32();
		this.SampleBits = stream.ReadInt32();
		var chunks = stream.ReadInt32(); // TODO it is possible that this is the number of channels. But when used as such, sound is speed up?
		stream.ReadUInt32(); // unk
		stream.Position += 32; // Empty
		stream.ReadBytes(20); // Filename
		this.data = stream.ReadBytes(size * chunks);
	}

	public Stream GetPCMInputStream()
	{
		return new MemoryStream(this.data);
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}
}
