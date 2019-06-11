using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Escalonador_SO_WPF
{
    /// <summary>
    /// Lógica interna para Informacoes.xaml
    /// </summary>
    public partial class Informacoes : Window
    {
        public Informacoes()
        {
            InitializeComponent();
            InicializarComponentesVisuais();
            Controle.Leitura_Arquivo();
            Controle.InicializarThreadCpu();
        }

        #region Inserção dos dados nas listViews
        /// <summary>
        /// Atualizar dados sem a necessidade de passar dados por parametro
        /// </summary>
        public void InicializarComponentesVisuais()
        {
            //Da pra transformar aquele tanto de listViewers em um vetor, por causa da referência dos objetos ListView
            Controle.listViewsFilasProcesso[0] = listViewPrioridade1;
            Controle.listViewsFilasProcesso[1] = listViewPrioridade2;
            Controle.listViewsFilasProcesso[2] = listViewPrioridade3;
            Controle.listViewsFilasProcesso[3] = listViewPrioridade4;
            Controle.listViewsFilasProcesso[4] = listViewPrioridade5;
            Controle.listViewsFilasProcesso[5] = listViewPrioridade6;
            Controle.listViewsFilasProcesso[6] = listViewPrioridade7;
            Controle.listViewsFilasProcesso[7] = listViewPrioridade8;
            Controle.listViewsFilasProcesso[8] = listViewPrioridade9;
            Controle.listViewsFilasProcesso[9] = listViewPrioridade10;
            Controle.listViewSuspensa = listViewSuspensos;
            Controle.listViewProntos = listViewProntos;

            Controle.label_CPU[0] = label_Estado_CPU_0;
            Controle.label_CPU[1] = label_Estado_CPU_1;
        }
        #endregion
    }
}
