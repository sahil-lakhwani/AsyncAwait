using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public class FileNameAndParentFrn
    {
        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        

        private UInt64 _parentFrn;
        public UInt64 ParentFrn
        {
            get { return _parentFrn; }
            set { _parentFrn = value; }
        }


        private UInt64 _frn;
        public UInt64 Frn
        {
            get { return _frn; }
            set { _frn = value; }
        }

        #endregion

        #region Constructor

        public FileNameAndParentFrn() { }
        public FileNameAndParentFrn(string name, UInt64 parentFrn, UInt64 Frn)
        {
            if (name != null && name.Length > 0)
            {
                _name = name;
            }
            else
            {
                throw new ArgumentException("Invalid argument: null or Length = zero", "name");
            }
            if (!(parentFrn < 0))
            {
                _parentFrn = parentFrn;
            }
            else
            {
                throw new ArgumentException("Invalid argument: less than zero", "parentFrn");
            }

            if (!(parentFrn < 0))
            {
                _frn = Frn;
            }
            else
            {
                throw new ArgumentException("Invalid argument: less than zero", "parentFrn");
            }

        }
        #endregion
    }
}
