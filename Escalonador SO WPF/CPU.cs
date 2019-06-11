using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Escalonador_SO_WPF
{
    class CPU
    {
        Processo process;
        bool processando;

        #region Monitor Joao
        public Processo GetProcesso
        {
            set // "processo entrando na cpu"
            {
                Monitor.Enter(this);

                if (processando) // se a cpu estiver ocupada, espera
                    Monitor.Wait(this);

                process = value;
                processando = true;

                for (int i = 0; i < process.Prioridade && i < process.QtdeCiclos; i++) // simular execução do threads repete a quantidade de quantuns
                {
                    Thread.Sleep(300);
                    process.DiminuirQtdeCiclos();
                }

                if (process.QtdeCiclos > 0)
                    process.DiminuirPrioridade();

                processando = false;

                Monitor.Pulse(this);
                Monitor.Exit(this);
            }
            get // "processo saindo da cpu"
            {
                Monitor.Enter(this);

                if (processando || process == null)
                    Monitor.Wait(this);

                Monitor.Exit(this);
                return process;
            }
        }

        #endregion

        /// <summary>
        /// Simula a execução de um processo, sendo que sua prioridade é o seu tempo de quantum
        /// </summary>
        /// <param name="processo">Processo que será executado</param>
        /// <returns>Retorna 1, se o processo completou sua execução
        /// Retorna -1, se o processo NÃO completou a sua execução</returns>
        public int Run(Processo processo)
        {
            int aux = 1; // representa quanto tempo passou

            while (aux <= processo.Prioridade && processo.QtdeCiclos > 0)
            {
                processo.DiminuirQtdeCiclos();
                Thread.Sleep(Controle.tempoSegundosQuantum*1000); //Simula o tempo gasto pela CPU, em segundos
                aux++;
            }

            if (processo.QtdeCiclos == 0)
                return 1;
            else
                processo.DiminuirPrioridade();

            return -1;
        }

    }
}
