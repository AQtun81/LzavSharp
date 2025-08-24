#ifdef _WIN32
  #define LZAV_INLINE __declspec(dllexport)
#else
  #define LZAV_INLINE __attribute__((visibility("default")))
#endif

#define LZAV_INLINE_F LZAV_INLINE