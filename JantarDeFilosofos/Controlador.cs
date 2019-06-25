using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JantarDeFilosofos
{
    class Controlador
    {

        private int tempoMaximo = 5;
        public int clock = 5000;
        public int clockTroca = 2000;
        public int numeroFilosofos { get; set;}
      
        // public Filosofo[] listaFilosofos = new Filosofo[5];
        public List<Filosofo> listaFilosofos = new List<Filosofo>();
        
        public Controlador()
        {
            this.numeroFilosofos = 5;
        }

        public void preencher()
        {
            //Adicionando filosofos na lista
            Filosofo aux = new Filosofo(0, null);
            listaFilosofos.Add(aux);
            for (int i = 1; i < numeroFilosofos; ++i)
            {

               aux = new Filosofo(i, aux);
               listaFilosofos.Add(aux);
                

                
            }
            listaFilosofos[0].anterior = listaFilosofos[numeroFilosofos - 1];

        }


        public Filosofo encontraFilosofoPorNome(string nome)
        {
            foreach (Filosofo a in listaFilosofos)
            {
                if (nome == a.nome)
                {
                    return a;
                }
            }
            return null;
        }

        public bool verificaDisponibilidade(Filosofo atual, Filosofo anterior)
        {
            if (atual == null || anterior == null) return false;

            if (atual.garfo.disponivel && anterior.garfo.disponivel) return true;

            else return false;
        }


    }
}
