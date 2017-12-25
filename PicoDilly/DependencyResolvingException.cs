using System;

namespace PicoDilly {
    public class DependencyResolvingException : Exception {
        public DependencyResolvingException(string message) : base(message) {
        }
    }
}