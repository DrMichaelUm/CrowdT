using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class GameConfigurationInformation
{
    public string UnityVersion;
    public string MayoVersion;
    public List<SDKTrackingVersion> TrackedSdkList;
    public List<SDKTrackingVersion> TrackedNetworkList;
}

[Serializable]
public class SDKTrackingVersion
{
    public string SDKName;
    public string SDKVersion;
}
