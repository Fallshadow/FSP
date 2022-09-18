namespace fsp.assetbundlecore
{
    public interface ISyncProxy
    {
        int GetHash();
        int GetABHash();
        int GetTypeHash();
        void Addreference();
        void DefReference();
        void Destroy();
        void ZeroReference();
    }
}