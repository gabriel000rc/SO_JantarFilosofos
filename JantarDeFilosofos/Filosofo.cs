using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JantarDeFilosofos
{
    class Filosofo
    {
        public String nome {get; set;}
        public int refeicoes;
        public Garfo garfo;
        public bool executado;
        public Filosofo anterior;



        public Filosofo( int numeroFilosofo, Filosofo filosofoAnterior)
        {
            this.anterior = filosofoAnterior;
            this.refeicoes = 0;
            this.garfo = new Garfo();
            this.nome = "filosofo " + numeroFilosofo;
        }

        public void Comer()
        {

        }

    }
}
