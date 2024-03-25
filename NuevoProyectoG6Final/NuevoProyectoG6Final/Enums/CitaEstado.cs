namespace NuevoProyectoG6Final.Enums
{
    using System.ComponentModel;

    public enum CitaEstado
    {
        [Description("Agendada")]
        AGENDADA,

        [Description("Cancelada")]
        CANCELADA,

        [Description("En Curso")]
        EN_CURSO,

        [Description("Finalizada")]
        FINALIZADA
    }

}
