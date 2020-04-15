using System;
using System.Collections;

namespace CMS.UNO.Framework.DataAccess
{
    public class clsParameterCollection : System.Collections.CollectionBase
    {

        public ParamStruct this[int index]
        {
            get
            {
                return (ParamStruct)List[index];

            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(ParamStruct value)
        {
            return List.Add(value);
        }

        public int IndexOf(ParamStruct value)
        {
            return List.IndexOf(value);
        }

        public void Insert(int index, ParamStruct value)
        {
            List.Insert(index, value);
        }

        public void Remove(ParamStruct value)
        {
            List.Remove(value);
        }

        public bool Contains(ParamStruct value)
        {
            return List.Contains(value);
        }

        protected override void OnInsert(int index, object value)
        {
            if (!(value.GetType() == Type.GetType("CMS.UNO.Framework.DataAccess.ParamStruct")))
            {

                throw new ArgumentException("value must be of type ParamStruct.", "value");
            }
        }

        protected override void OnRemove(int index, object value)
        {
            if (!(value.GetType() == Type.GetType("CMS.UNO.Framework.DataAccess.ParamStruct")))
            {
                throw new ArgumentException("value must be of type ParamStruct.", "value");
            }
        }

        protected override void OnSet(int index, object oldValue, object newValue)
        {
            if (!(newValue.GetType() == Type.GetType("CMS.UNO.Framework.DataAccess.ParamStruct")))
            {
                throw new ArgumentException("newValue must be of type ParamStruct.", "newValue");
            }
        }

        protected override void OnValidate(object value)
        {
            if (!(value.GetType() == Type.GetType("CMS.UNO.Framework.DataAccess.ParamStruct")))
            {
                throw new ArgumentException("value must be of type ParamStruct.");
            }
        }
    }
}
