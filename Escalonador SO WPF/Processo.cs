using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todas_as_Estruturas_de_Dados;

namespace Escalonador_SO_WPF
{
    class Processo : IDado
    {
        public int PID { get; private set; }
        public string Nome { get; private set; }
        public int Prioridade { get; private set; }
        public int QtdeCiclos { get; set; }

        public Processo(int PID, string Nome, int Prioridade, int QtdeCiclos)
        {
            this.PID = PID;
            this.Nome = Nome;
            this.Prioridade = Prioridade;
            this.QtdeCiclos = QtdeCiclos;
        }

        /// <summary>
        /// Diminui a quantidade total de ciclos em 1 ciclo.
        /// </summary>
        public void DiminuirQtdeCiclos()
        {
            if (QtdeCiclos > 0)
            QtdeCiclos--;
        }

        /// <summary>
        /// Diminui a prioridade, aumentando o valor da prioridade
        /// </summary>
        public void DiminuirPrioridade()
        {
            if (Prioridade < 10)
                Prioridade++;
        }
        /// <summary>
        /// Aumenta a prioridade, diminuindo o valor da prioridade
        /// </summary>
        public void AumentarPrioridade()
        {
            if (Prioridade > 1)
                Prioridade--;
        }

        /// <summary>
        /// Informação do objeto
        /// </summary>
        /// <returns>string contendo as informações do objeto</returns>
        public override string ToString()
        {
            return string.Format(" PID: {0} Nome: {1} Prioridade: {2} Quantidade de Ciclos: {3}.", PID, Nome, Prioridade, QtdeCiclos);
        }

        /// <summary>
        /// Usado para criar a árvore
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Processo auxProcesso = (Processo)obj;

            if (this.PID < auxProcesso.PID)
                return -1;
            else if (this.PID == auxProcesso.PID)
                return 0;
            else
                return 1;
        }
    }
}
