using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Escalonador_SO_WPF
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Controle.InicializarThreadAdicional();
        }

        #region Botões
        private void Button_Click_Iniciar_Novo_Ciclo_de_Execucao(object sender, RoutedEventArgs e)
        {
            //Falta reiniciar as threads
            Controle.escalonador = new Escalonador();

            Controle.listViewsFilasProcesso[0].Dispatcher.Invoke(DispatcherPriority.Normal,
            new Action(() =>
            {
                for (int pos = 0; pos < Controle.listViewsFilasProcesso.Length; pos++)
                    Controle.listViewsFilasProcesso[pos].Items.Clear();
                Controle.listViewProntos.Items.Clear();
                Controle.listViewSuspensa.Items.Clear();

                Controle.label_CPU[0].Content = "";
                Controle.label_CPU[1].Content = "";
            }
            ));

            Controle.Leitura_Arquivo();
        }

        private void Button_Click_Suspender_Processo(object sender, RoutedEventArgs e)
        {
            //como fazer?
        }

        private void Button_Click_Resumir_Processo(object sender, RoutedEventArgs e)
        {
            //retira objeto que está dentro da listView e insere no escalonador
        }
        
        private void Button_Click_Sair(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void Button_Click_Modificar_Prioridade(object sender, RoutedEventArgs e)
        {
            //Como fazer isso?
        }
    }
}
