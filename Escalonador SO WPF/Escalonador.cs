using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Todas_as_Estruturas_de_Dados;

namespace Escalonador_SO_WPF
{
    class Escalonador
    {
        public Fila[] TodasFilasProcessos { get; set; }

        public Escalonador()
        {
            TodasFilasProcessos = new Fila[10];

            for (int i = 0; i < TodasFilasProcessos.Length; i++)
                TodasFilasProcessos[i] = new Fila();
        }

        /// <summary>
        /// Simula a execução do escalonador
        /// </summary>
        public void Run()
        {
            CPU cpu = new CPU();
            int pos = 0;
            while (!TodosVazia() && pos < TodasFilasProcessos.Length) // Enquanto todas as filas de processos não estiverem vazias
            {
                while (!TodasFilasProcessos[pos].Vazio()) // Enquanto uma fila especifica não estiver vazia
                {
                    //Retirar da fila e da listView
                    Monitor.Enter(this);

                    Processo processo = (Processo)(TodasFilasProcessos[pos].Retirar());

                    if (processo == null) //alguma thread ficou presa dentro do while RESOLVER ISSO
                        break;

                    Controle.RetirarListViewPrioridades(processo);

                    if (Thread.CurrentThread.Name == Controle.thread_cpu0.Name)
                        Controle.TrocarLabel("Rodando: " + processo.Nome, 0);
                    else if(Thread.CurrentThread.Name == Controle.thread_cpu1.Name)
                        Controle.TrocarLabel("Rodando: " + processo.Nome, 1);

                    Monitor.Exit(this);

                    int reposta = cpu.Run(processo);

                    Monitor.Enter(this);
                    if (reposta < 0)
                    {
                        //não terminou
                        AdicionarProcesso(processo);
                        Controle.InserirListViewPrioridades(processo);
                    }
                    else if (reposta == 0)
                    {
                        //suspendeu
                        AdicionarProcesso(processo);
                        Controle.InserirListViewPrioridades(processo);
                    }
                    else
                    {
                        //terminou
                        Controle.InserirListViewPronto(processo);
                    }
                    Monitor.Exit(this);
                }
                pos++;
            }

            if (Thread.CurrentThread.Name == Controle.thread_cpu0.Name)
                Controle.TrocarLabel("Fim", 0);
            else if (Thread.CurrentThread.Name == Controle.thread_cpu1.Name)
                Controle.TrocarLabel("Fim", 0);

            System.Windows.Threading.Dispatcher.Run();
        }


        /// <summary>
        /// Insere o processo na fila de prioridade correta
        /// </summary>
        /// <param name="processo">Processo que será inserido</param>
        public void AdicionarProcesso(Processo processo)
        {
            switch (processo.Prioridade)
            {
                case 1: TodasFilasProcessos[0].Inserir(processo); break;
                case 2: TodasFilasProcessos[1].Inserir(processo); break;
                case 3: TodasFilasProcessos[2].Inserir(processo); break;
                case 4: TodasFilasProcessos[3].Inserir(processo); break;
                case 5: TodasFilasProcessos[4].Inserir(processo); break;
                case 6: TodasFilasProcessos[5].Inserir(processo); break;
                case 7: TodasFilasProcessos[6].Inserir(processo); break;
                case 8: TodasFilasProcessos[7].Inserir(processo); break;
                case 9: TodasFilasProcessos[8].Inserir(processo); break;
                case 10: TodasFilasProcessos[9].Inserir(processo); break;
                default: break;
            }
        }

        /// <summary>
        /// Verifica se todas as filas estão vazias
        /// </summary>
        /// <returns>True, se todas estão vazias. False, se existe um processo dentro delas</returns>
        public bool TodosVazia()
        {
            return TodasFilasProcessos[0].Vazio() && TodasFilasProcessos[1].Vazio() && TodasFilasProcessos[2].Vazio() && TodasFilasProcessos[3].Vazio() && TodasFilasProcessos[4].Vazio() && TodasFilasProcessos[5].Vazio() && TodasFilasProcessos[6].Vazio() && TodasFilasProcessos[7].Vazio() && TodasFilasProcessos[8].Vazio() && TodasFilasProcessos[9].Vazio();
        }

        /// <summary>
        /// Ta de brincation wit me?
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder auxImpressao = new StringBuilder();

            for (int pos = 0; pos < TodasFilasProcessos.Length; pos++)
            {
                auxImpressao.AppendLine("\tPrioridade " + (pos + 1));
                auxImpressao.AppendLine(TodasFilasProcessos[pos].ToString());
            }

            return auxImpressao.ToString();
        }
    }
}
