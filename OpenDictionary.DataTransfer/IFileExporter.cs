using System.Threading.Tasks;

namespace OpenDictionary.DataTransfer;

public interface IFileExporter
{
    ValueTask<IReadOnlyCacheContainer<IFile>> AsSingleFile<T>(FileData<T>[] datas);
    ValueTask<IReadOnlyCacheContainer<IFile>> AsMultipleFiles<T>(FileData<T>[] datas);
}