using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Gov.Core.Entity.Elezioni
{
    public class Voti
    {
        public Voti(){
        }
      
        public Voti(int bianche,int contestate, int municipio, int nulle,int soloSindaco, int totale, int totaleValide, int sezioniPervenute,int iscritti)
        {
            Bianche = bianche;
            Contestate = contestate;
            Municipio = municipio;
            Nulle = nulle;
            SoloSindaco = soloSindaco;
            Totale = totale;
            TotaleValide = totaleValide;
            SezioniPervenute = sezioniPervenute;
            Iscritti = iscritti;
        }
        public override int GetHashCode()
           => HashCode.Combine(Municipio, SezioniPervenute, Iscritti);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Voti)obj);
        }
        protected bool Equals(Voti other)
        {
            return Municipio.Equals(other.Municipio);
        }

        public int Bianche { get; set; }
        public int Contestate { get; set; }
        public int Municipio { get; set; }
        public int Nulle { get; set; }

        public int Iscritti { get; set; }
        public int SezioniPervenute { get; set; }
        public int SoloSindaco { get; set; }
        public int Totale { get; set; }
        public int TotaleValide { get; set; }
       
       
    }
}
