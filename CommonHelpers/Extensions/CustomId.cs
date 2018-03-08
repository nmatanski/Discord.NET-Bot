using System;

namespace CommonHelpers.Extensions
{
    public class CustomId
    {
        private Guid _id;

        public CustomId()
        {
            _id = Guid.NewGuid();
        }

        public CustomId(Guid guid)
        {
            _id = guid;
        }

        public override string ToString()
        {
            return _id.ToString();
        }
    }
}
