namespace ES.Engine.PrePostProcessing
{
    public interface IProcessor<T>
    {
        T ApplyProcessing(T objectToProcess);
    }
}
