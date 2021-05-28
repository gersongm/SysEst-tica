namespace Domain.ValueObjects
{
    //PRIMERAMENTE VAMOS A CAMBIAR LA CLASE POR ENUMERACIÓN
    public enum EntityState
    {
        //ESTE ELEMENTO NOS SERVIRÁ PARA ESPECIFICAR EL ESTADO DE LAS ENTIDADES, EN EL QUE DEFINIREMOS TRES ESTADOS, ADDED, MODIFIED Y DELETED
        Added,
        Modified,
        Deleted
    }
}
