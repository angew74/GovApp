using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Core.Entity.Elezioni
{
   public class Calcolo: IEnumerable<Voti>
    {
       

        IEnumerator<Voti> IEnumerable<Voti>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public void AddVoti(Voti voti)
        {

            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
