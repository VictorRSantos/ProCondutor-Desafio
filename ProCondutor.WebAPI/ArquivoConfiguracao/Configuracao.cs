using ProCondutor.WebAPI.Enums;
using System;

namespace ProCondutor.WebAPI.ArquivoConfiguracao
{
    public class Configuracao
    {
        public string ConexaoBancoSQLServer { get; set; }

        public string BancoSelecionado { get; set; }

        public TipoBanco TipoBanco { get => TipoBancoSelecionado(BancoSelecionado); }

        private TipoBanco TipoBancoSelecionado(string bancoSelecionado)
        {
            Enum.TryParse(bancoSelecionado, out TipoBanco bancoselecionado);

            return bancoselecionado;
        }
    }
}
