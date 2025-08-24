using System.Runtime.InteropServices;

namespace LzavSharp;

public static partial class Lzav
{
    /* COMPRESS DEFAULT
     -------------------------------------------------------------------------------------------------*/
    
    [DllImport(LZAV_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "lzav_compress_bound")]
    public static extern int CompressBound(int sourceLength);
    
    /// <returns>length of compressed data in the destination buffer, negative value indicates a failure</returns>
    [DllImport(LZAV_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "lzav_compress_default")]
    public static extern int CompressDefault(IntPtr source, IntPtr destination, int sourceLength, int destinationLength);

    /// <returns>length of compressed data in the destination buffer, negative value indicates a failure</returns>
    public static unsafe int CompressDefault(in Span<byte> source, Span<byte> destination)
    {
        fixed (byte* srcPtr = &MemoryMarshal.GetReference(source), dstPtr = &MemoryMarshal.GetReference(destination))
        {
            return CompressDefault((IntPtr)srcPtr, (IntPtr)dstPtr, source.Length, destination.Length);
        }
    }

    /// <returns>length of compressed data in the output buffer, negative value indicates a failure</returns>
    public static unsafe int CompressDefault(in Span<byte> source, out Span<byte> output)
    {
        output = new byte[CompressBound(source.Length)];
        fixed (byte* srcPtr = &MemoryMarshal.GetReference(source), dstPtr = &MemoryMarshal.GetReference(output))
        {
            return CompressDefault((IntPtr)srcPtr, (IntPtr)dstPtr, source.Length, output.Length);
        }
    }
    
    /* COMPRESS HIGH
     -------------------------------------------------------------------------------------------------*/
    
    [DllImport(LZAV_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "lzav_compress_bound_hi")]
    public static extern int CompressBoundHi(int sourceLength);
    
    /// <returns>length of compressed data in the destination buffer, negative value indicates a failure</returns>
    [DllImport(LZAV_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "lzav_compress_hi")]
    public static extern int CompressHi(IntPtr source, IntPtr destination, int sourceLength, int destinationLength);

    /// <returns>length of compressed data in the destination buffer, negative value indicates a failure</returns>
    public static unsafe int CompressHi(in Span<byte> source, Span<byte> destination)
    {
        fixed (byte* srcPtr = &MemoryMarshal.GetReference(source), dstPtr = &MemoryMarshal.GetReference(destination))
        {
            return CompressHi((IntPtr)srcPtr, (IntPtr)dstPtr, source.Length, destination.Length);
        }
    }

    /// <returns>length of compressed data in the output buffer, negative value indicates a failure</returns>
    public static unsafe int CompressHi(in Span<byte> source, out Span<byte> output)
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
    [DllImport(LZAV_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "lzav_decompress")]
    public static extern int Decompress(IntPtr source, IntPtr destination, int sourceLength, int destinationLength);

    /// <returns>length of decompressed data in the destination buffer, negative value indicates a failure</returns>
    public static unsafe int Decompress(in Span<byte> source, Span<byte> destination)
    {
        fixed (byte* srcPtr = &MemoryMarshal.GetReference(source), dstPtr = &MemoryMarshal.GetReference(destination))
        {
            return Decompress((IntPtr)srcPtr, (IntPtr)dstPtr, source.Length, destination.Length);
        }
    }
    
    /// <returns>length of decompressed data in the output buffer, negative value indicates a failure</returns>
    public static unsafe int Decompress(in Span<byte> source, int decompressedSize, out Span<byte> output)
    {
        output = new byte[decompressedSize];
        fixed (byte* srcPtr = &MemoryMarshal.GetReference(source), dstPtr = &MemoryMarshal.GetReference(output))
        {
            return Decompress((IntPtr)srcPtr, (IntPtr)dstPtr, source.Length, output.Length);
        }
    }
}