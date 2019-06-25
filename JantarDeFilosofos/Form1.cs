using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JantarDeFilosofos
{
    public partial class Form1 : Form
    {
        Controlador controle = new Controlador();

        public Form1()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Cria os filosofos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void botaoPreencher(object sender, EventArgs e)
        {

            // Cria lista de Filosofos
            controle.preencher();
          
            // Preenche o painel de Filosofos pensando 
            foreach(var a in controle.listaFilosofos)
            {
                var aux = crialControlFilosofo(a);
                painel1.Controls.Add(aux);
                
            }

            // Preenche o painel de Garfos disponiveis
            foreach (var a in controle.listaFilosofos)
            {
                var aux = crialControlGarfo(a);
                painel2.Controls.Add(aux);
            }         


        }

        private void button2_Click(object sender, EventArgs e)
        {
            // limpa todas as listas 
            controle.listaFilosofos.Clear();
            painel1.Controls.Clear();
            painel2.Controls.Clear();
            painel3.Controls.Clear();
            painel4.Controls.Clear();
            painel5.Controls.Clear();
            relogio.Text = "0";
        }

        private void botaoExecutar(object sender, EventArgs e)
        {
            //verifica se tem filosofos
            if (controle.listaFilosofos.Count() < 2)
            {
                MessageBox.Show("Sem filosofos suficientes");
                return;
            }
            int i = 0;

            // 
            while (i < 5)

            {
                // Percorre a lista de filosofos e faz os disponiveis comerem
                foreach (Filosofo a in controle.listaFilosofos)
                {

                    fazRefeicao(a);
                }


                // adiciona o tempo (5s) ao timer
                MessageBox.Show("Pausa de 5s");
                relogio.Text = Convert.ToString(Convert.ToInt64(relogio.Text) + 5);


                for (int j = 0; j < controle.listaFilosofos.Count(); ++j)
                {
                    for (int k = 0; k < 2; ++k)
                    {
                        if (controle.listaFilosofos.Count() == 1)
                        {
                            var u = controle.listaFilosofos[0];
                            // Manda para o painel de satisfeitos
                            painel3.Controls.RemoveByKey(u.nome);
                            painel5.Controls.Add(crialControlFilosofo(u));

                            // Retorno os garfos para disponivel(atual e o do filosofo anterior)
                            u.garfo.disponivel = true;
                            u.anterior.garfo.disponivel = true;

                            garfoDisponivel(u);
                            garfoDisponivel(u.anterior);

                            //                            
                            controle.listaFilosofos.Remove(u);
                        }
                        else
                        {
                            var a = controle.listaFilosofos[k];
                            if (a.executado)
                            {
                                a.executado = false;
                                if (a.refeicoes < 2)
                                {

                                    // volta para o painel pensando
                                    painel3.Controls.RemoveByKey(a.nome);
                                    painel1.Controls.Add(crialControlFilosofo(a));

                                    // Retorno os garfos para disponivel(atual e o do filosofo anterior)
                                    a.garfo.disponivel = true;
                                    a.anterior.garfo.disponivel = true;

                                    // Se for menos q duas refeicoes, ele vai para o fim da fila
                                    controle.listaFilosofos.Remove(a);
                                    controle.listaFilosofos.Add(a);

                                    /////
                                    garfoDisponivel(a);
                                    garfoDisponivel(a.anterior);
                                }
                                else if (a.refeicoes == 2)
                                {
                                    // Manda para o painel de satisfeitos
                                    painel3.Controls.RemoveByKey(a.nome);
                                    painel5.Controls.Add(crialControlFilosofo(a));

                                    // Retorno os garfos para disponivel(atual e o do filosofo anterior)
                                    a.garfo.disponivel = true;
                                    a.anterior.garfo.disponivel = true;

                                    garfoDisponivel(a);
                                    garfoDisponivel(a.anterior);

                                    //                            
                                    controle.listaFilosofos.Remove(a);

                                }
                            }
                        }
                    }
                }
                MessageBox.Show("Pausa de 2s");
                ++i;
            }
        }




        ///**************************************************************************************************

        /// <summary>
        ///  Cria um controle de um garfo(utilizado para fazer a interface)
        /// </summary>
        /// <param name="a"></param>
        /// <returns> Control </returns> 
        private Control crialControlGarfo(Filosofo a)
        {
            var aux = new Label();
            aux.Name = ("garfo de: " + Convert.ToString(a.nome));
            aux.Text = ("garfo de: " + Convert.ToString(a.nome));
            return aux;
        }


        /// <summary>
        /// Cria um controle de um filosofo(utilizado para fazer a interface)
        /// </summary>
        /// <param name="a"></param>
        /// <returns> Control </returns> 
        private Control crialControlFilosofo(Filosofo a)
        {
            var aux = new Label();
            aux.Name = (Convert.ToString(a.nome));
            aux.Text = (Convert.ToString(a.nome));
            return aux;
        }

        /// <summary>
        /// Manipula os garfos tirandoos do painel1(disponivel) e passando  para o painel4(ocupados)
        /// </summary>
        /// <param name="a">filosofo o qual o garfo sera disponibilizado </param>
        private void garfoDisponivel(Filosofo a)
        {
            
            var aux2 = crialControlGarfo(a);

            painel2.Controls.Add(aux2);
            painel4.Controls.RemoveByKey(aux2.Name);
        }

        /// <summary>
        /// Manipula os garfos tirandoos do painel4(Ocupado) e passando  para o painel4(Disponivel)
        /// </summary>
        /// <param name="a">filosofo o qual o garfo sera Utilizado </param>
        private void garfoUtilizado(Filosofo a)
        {

            var aux2 = crialControlGarfo(a);

            painel4.Controls.Add(aux2);
            painel2.Controls.RemoveByKey(aux2.Name);
        }

        /// <summary>
        /// verifica os filosofos com recursos disponiveis e aloca os recursos
        /// </summary>
        /// <param name="atual"></param>
        private void fazRefeicao(Filosofo atual)
        {
            if (controle.verificaDisponibilidade(atual, atual.anterior))
            {
                atual.refeicoes++;
                atual.executado = true;
                atual.garfo.disponivel = false;
                atual.anterior.garfo.disponivel = false;

                var aux2 = crialControlFilosofo(atual);


                painel3.Controls.Add(aux2);
                painel1.Controls.RemoveByKey(aux2.Name);

                garfoUtilizado(atual.anterior);
                garfoUtilizado(atual);
            }

        }
    }
}
