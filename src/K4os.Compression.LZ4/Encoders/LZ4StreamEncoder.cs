﻿namespace K4os.Compression.LZ4.Encoders
{
	public class LZ4StreamEncoder: ILZ4StreamEncoder
	{
		private readonly ILZ4StreamEncoder _encoder;

		public LZ4StreamEncoder(LZ4Level level, int blockSize, int extraBlocks)
		{
			_encoder = Create(level, blockSize, extraBlocks);
		}

		public static ILZ4StreamEncoder Create(LZ4Level level, int blockSize, int extraBlocks) =>
			level == LZ4Level.L00_FAST
				? CreateFastEncoder(blockSize, extraBlocks)
				: CreateHighEncoder(level, blockSize, extraBlocks);

		private static ILZ4StreamEncoder CreateHighEncoder(
			LZ4Level level, int blockSize, int extraBlocks) =>
			new LZ4HighStreamEncoder(level, blockSize, extraBlocks);

		private static ILZ4StreamEncoder CreateFastEncoder(int blockSize, int extraBlocks) =>
			new LZ4FastStreamEncoder(blockSize, extraBlocks);

		public int BlockSize => _encoder.BlockSize;
		public int BytesReady => _encoder.BytesReady;

		public unsafe int Topup(byte* source, int length) => _encoder.Topup(source, length);
		public unsafe int Encode(byte* target, int length) => _encoder.Encode(target, length);
		public unsafe int Copy(byte* target, int length) => _encoder.Copy(target, length);

		public void Dispose() => _encoder.Dispose();
	}
}
