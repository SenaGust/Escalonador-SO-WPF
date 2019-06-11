using System;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Escalonador_SO_WPF
{
    class Controle
    {
        #region Thread
        public static Thread thread_cpu0 = new Thread(() => escalonador.Run());
        public static Thread thread_cpu1 = new Thread(() => escalonador.Run());
        public static Thread thread_Adicional = new Thread(() => AbrirJanelaInformacoes());

        /// <summary>
        /// Inicializando Thread adicional
        /// </summary>
        public static void InicializarThreadAdicional()
        {
            Controle.thread_Adicional.Name = "Thread Adicional";
            Controle.thread_Adicional.SetApartmentState(ApartmentState.STA);
            Controle.thread_Adicional.IsBackground = true;
            Controle.thread_Adicional.Start();
        }

        /// <summary>
        /// Inicializando as Thread Que representam as 2 CPU's
        /// </summary>
        public static void InicializarThreadCpu()
        {
            Controle.thread_cpu0.Name = "Thread CPU 0";
            Controle.thread_cpu0.SetApartmentState(ApartmentState.STA);
            Controle.thread_cpu0.IsBackground = true;
            Controle.thread_cpu0.Start();


            Controle.thread_cpu1.Name = "Thread CPU 1";
            Controle.thread_cpu1.SetApartmentState(ApartmentState.STA);
            Controle.thread_cpu1.IsBackground = true;
            Controle.thread_cpu1.Start();
        }

        /// <summary>
        /// Abre a janela com as informações usando uma thread
        /// </summary>
        public static void AbrirJanelaInformacoes()
        {
            Informacoes newWindow = new Informacoes();
            newWindow.Show();
            System.Windows.Threading.Dispatcher.Run();
        }
        #endregion

        #region Escalonador
        public static Escalonador escalonador = new Escalonador();
        public static readonly int tempoSegundosQuantum = 1;
        public static readonly string nomeArquivo = "processos.txt";

        public static void Leitura_Arquivo()
        {
            if (!File.Exists(nomeArquivo))
            {
                //Exibir mensagem de erro "Arquivo não foi encontrado ou não existe."	

                return;
            }

            //Fazer a leitura do arquivo e organizar entre as 10 filas	
            using (StreamReader entrada = new StreamReader(nomeArquivo))
            {
                string[] info;

                while (!entrada.EndOfStream)
                {
                    info = entrada.ReadLine().Split(';');
                    try
                    {
                        if (info.Length == 4)
                        {
                            Processo processo = new Processo(Convert.ToInt32(info[0]), info[1], Convert.ToInt32(info[2]), Convert.ToInt32(info[3]));
                            escalonador.AdicionarProcesso(processo);
                            InserirListViewPrioridades(processo);
                        }
                        else
                        {
                            //Exibir mensagem de erro "falta atributos"	

                        }
                    }
                    catch (Exception erro)
                    {
                        //Exibir mensagem de erro de conversão	

                    }
                }
            }
        }
        #endregion

        #region Labels
        public static Label[] label_CPU = new Label[2];
        /// <summary>
        /// Atualiza label com o nome do processo que está rodando
        /// </summary>
        /// <param name="processo">Processo que aparecerá na label</param>
        /// <param name="cpu">qual CPU está sendo usada</param>
        public static void TrocarLabel(string texto, int cpu)
        {
            label_CPU[cpu].Dispatcher.Invoke(DispatcherPriority.Normal,
            new Action(() => {
                label_CPU[cpu].Content = texto;
            }
            ));
        }
        #endregion

        #region ListViews
        public static ListView listViewSuspensa;
        public static ListView listViewProntos;
        public static ListView[] listViewsFilasProcesso = new ListView[10];

        /// <summary>
        /// Adiciona o processo ao listView Correto
        /// </summary>
        /// <param name="processo">Processo que será adicionado ao listView</param>
        public static void InserirListViewPrioridades(Processo processo)
        {
            listViewsFilasProcesso[processo.Prioridade - 1].Dispatcher.Invoke(DispatcherPriority.Normal,
            new Action(() => {
                listViewsFilasProcesso[processo.Prioridade - 1].Items.Add(processo);
            }
            ));
        }

        /// <summary>
        /// Retira o processo na fila de processos
        /// </summary>
        /// <param name="processo">Processo que será retirado</param>
        public static void RetirarListViewPrioridades(Processo processo)
        {
            listViewsFilasProcesso[processo.Prioridade - 1].Dispatcher.Invoke(DispatcherPriority.Normal, 
            new Action(() => {
                listViewsFilasProcesso[processo.Prioridade - 1].Items.Remove(processo);
            }
            ));
        }

        /// <summary>
        /// Insere o processo na listView com processos que foram suspensos
        /// </summary>
        /// <param name="processo">Processo que será enviado para a listView Suspesos</param>
        public static void InserirListViewSuspensa(Processo processo)
        {
            listViewSuspensa.Dispatcher.Invoke(DispatcherPriority.Normal,
            new Action(() => {
                listViewSuspensa.Items.Add(processo);
            }
            ));
        }

        /// <summary>
        /// Insere o processo na listView com processos que terminaram
        /// </summary>
        /// <param name="processo">Processo que será enviado para a listView prontos</param>
        public static void InserirListViewPronto(Processo processo)
        {
            listViewSuspensa.Dispatcher.Invoke(DispatcherPriority.Normal,
            new Action(() => {
                listViewProntos.Items.Add(processo);
            }
            ));
        }
        #endregion
    }
}
