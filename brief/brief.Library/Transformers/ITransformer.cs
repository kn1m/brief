namespace brief.Library.Transformers
{
    using System.Threading.Tasks;

    public interface ITransformer<in TSource, TResult> where TSource :  class
        where TResult : class
    {
        TResult Trasform(TSource source, params object[] configurations);
        Task<TResult> TransformAsync(TSource source, params object[] configurations);
    }
}