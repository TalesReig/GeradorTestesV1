namespace GeradorTeste.WinApp
{
    public abstract class ConfiguracaoToolboxBase
    {
        public abstract string TipoCadastro { get; }

        public abstract string TooltipInserir { get; }

        public abstract string TooltipEditar { get; }

        public abstract string TooltipExcluir { get; }

        public virtual string TooltipAdicionarItens { get; }

        public virtual string TooltipAtualizarItens { get; }

        public virtual string TooltipFiltrar { get; }

        public virtual string TooltipGerarPdf { get; }

        public virtual string TooltipDuplicar { get; }

        public virtual string TooltipVisualizar { get; }

        public virtual bool InserirHabilitado { get { return true; } }

        public virtual bool EditarHabilitado { get { return true; } }

        public virtual bool ExcluirHabilitado { get { return true; } }

        public virtual bool AdicionarItensHabilitado { get { return false; } }

        public virtual bool AtualizarItensHabilitado { get { return false; } }

        public virtual bool FiltrarHabilitado { get { return false; } }

        public virtual bool GerarPdfHabilitado { get { return false; } }

        public virtual bool DuplicarHabilitado { get { return false; } }

        public virtual bool VisualizarHabilitado { get { return false; } }

    }
}