#ifdef _WIN32
  #define LZAV_EXPORT __declspec(dllexport)
#else
  #define LZAV_EXPORT __attribute__((visibility("default")))
#endif
