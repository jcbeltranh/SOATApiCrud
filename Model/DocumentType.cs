using System.ComponentModel;

namespace SOATApiReact.Model
{
    public enum DocumentType
    {
        [Description("Cédula de ciudadanía")]
        CC,
        [Description("Cédula de extranjería")]
        CE,
        [Description("Tarjeta de identidad")]
        TI,
        [Description("Número de Identificacion Tributaria")]
        NIT,
        [Description("Documento Diplomático")]
        DIP
    }
}