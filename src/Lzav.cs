using System;
using System.Runtime.InteropServices;

namespace AQtun.LZAV
{
    public static partial class Lzav
    {
        #if NET5_0_OR_GREATER || NETCOREAPP || NETSTANDARD
        private const string LZAV_DLL = "lzav";
        #else // NETFRAMEWORK
        private const string LZAV_DLL = "lzav.dll";
        #endif
        
        /* COMPRESS DEFAULT
         -------------------------------------------------------------------------------------------------*/

        #if NET7_0_OR_GREATER
        [LibraryImport(LZAV_DLL, EntryPoint = "lzav_compress_bound")]
        public static partial int CompressBound(int sourceLength);
        #else
        [DllImport(LZAV_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "lzav_compress_bound")]
        public static extern int CompressBound(int sourceLength);
        #endif

        /// <returns>length of compressed data in the destination buffer, negative value indicates a failure</returns>
        #if NET7_0_OR_GREATER
        [LibraryImport(LZAV_DLL, EntryPoint = "lzav_compress_default")]
        public static partial int CompressDefault(IntPtr source, IntPtr destination, int sourceLength, int destinationLength);
        #else
        [DllImport(LZAV_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "lzav_compress_default")]
        public static extern int CompressDefault(IntPtr source, IntPtr destination, int sourceLength, int destinationLength);
        #endif

        /// <returns>length of compressed data in the destination buffer, negative value indicates a failure</returns>
        public static unsafe int CompressDefault(ReadOnlySpan<byte> source, Span<byte> destination)
        {
            fixed (byte* srcPtr = &MemoryMarshal.GetReference(source), dstPtr =
                       &MemoryMarshal.GetReference(destination))
            {
                return CompressDefault((IntPtr)srcPtr, (IntPtr)dstPtr, source.Length, destination.Length);
            }
        }

        /// <returns>length of compressed data in the output buffer, negative value indicates a failure</returns>
        public static unsafe int CompressDefault(ReadOnlySpan<byte> source, out Span<byte> output)
        {
            output = new byte[CompressBound(source.Length)];
            fixed (byte* srcPtr = &MemoryMarshal.GetReference(source), dstPtr = &MemoryMarshal.GetReference(output))
            {
                return CompressDefault((IntPtr)srcPtr, (IntPtr)dstPtr, source.Length, output.Length);
            }
        }

        /* COMPRESS HIGH
         -------------------------------------------------------------------------------------------------*/

        #if NET7_0_OR_GREATER
        [LibraryImport(LZAV_DLL, EntryPoint = "lzav_compress_bound_hi")]
        public static partial int CompressBoundHi(int sourceLength);
        #else
        [DllImport(LZAV_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "lzav_compress_bound_hi")]
        public static extern int CompressBoundHi(int sourceLength);
        #endif

        /// <returns>length of compressed data in the destination buffer, negative value indicates a failure</returns>
        #if NET7_0_OR_GREATER
        [LibraryImport(LZAV_DLL, EntryPoint = "lzav_compress_hi")]
        public static partial int CompressHi(IntPtr source, IntPtr destination, int sourceLength, int destinationLength);
        #else
        [DllImport(LZAV_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "lzav_compress_hi")]
        public static extern int CompressHi(IntPtr source, IntPtr destination, int sourceLength, int destinationLength);
        #endif

        /// <returns>length of compressed data in the destination buffer, negative value indicates a failure</returns>
        public static unsafe int CompressHi(ReadOnlySpan<byte> source, Span<byte> destination)
        {
            fixed (byte* srcPtr = &MemoryMarshal.GetReference(source), dstPtr = &MemoryMarshal.GetReference(destination))
            {
                return CompressHi((IntPtr)srcPtr, (IntPtr)dstPtr, source.Length, destination.Length);
            }
        }

        /// <returns>length of compressed data in the output buffer, negative value indicates a failure</returns>
        public static unsafe int CompressHi(ReadOnlySpan<byte> source, out Span<byte> output)
        {
            output = new byte[CompressBoundHi(source.Length)];
            fixed (byte* srcPtr = &MemoryMarshal.GetReference(source), dstPtr = &MemoryMarshal.GetReference(output))
            {
                return CompressHi((IntPtr)srcPtr, (IntPtr)dstPtr, source.Length, output.Length);
            }
        }

        /* DECOMPRESS
         -------------------------------------------------------------------------------------------------*/

        /// <returns>length of decompressed data in the destination buffer, negative value indicates a failure</returns>
        #if NET7_0_OR_GREATER
        [LibraryImport(LZAV_DLL, EntryPoint = "lzav_decompress")]
        public static partial int Decompress(IntPtr source, IntPtr destination, int sourceLength, int destinationLength);
        #else
        [DllImport(LZAV_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "lzav_decompress")]
        public static extern int Decompress(IntPtr source, IntPtr destination, int sourceLength, int destinationLength);
        #endif

        /// <returns>length of decompressed data in the destination buffer, negative value indicates a failure</returns>
        public static unsafe int Decompress(ReadOnlySpan<byte> source, Span<byte> destination)
        {
            fixed (byte* srcPtr = &MemoryMarshal.GetReference(source), dstPtr = &MemoryMarshal.GetReference(destination))
            {
                return Decompress((IntPtr)srcPtr, (IntPtr)dstPtr, source.Length, destination.Length);
            }
        }

        /// <returns>length of decompressed data in the output buffer, negative value indicates a failure</returns>
        public static unsafe int Decompress(ReadOnlySpan<byte> source, int decompressedSize, out Span<byte> output)
        {
            output = new byte[decompressedSize];
            fixed (byte* srcPtr = &MemoryMarshal.GetReference(source), dstPtr = &MemoryMarshal.GetReference(output))
            {
                return Decompress((IntPtr)srcPtr, (IntPtr)dstPtr, source.Length, output.Length);
            }
        }
    }
}