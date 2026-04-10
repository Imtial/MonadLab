1. If a function throws exception, suffix it's signature with `OrThrow`
2. Only use exception when you want the application to break and do not want to recover - neither automatically, nor from a different input.
3. If a function returns Result<T, E>, try your best not to throw from that function.