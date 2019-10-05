using EvoS.Framework.DataAccess;
using EvoS.Framework.Network;
using System;

// Token: 0x020009D9 RID: 2521
[Serializable]
[EvosMessage(769, typeof(ServerMessageOverrides))]
public class ServerMessageOverrides
{
    public ServerMessage FacebookOAuthRedirectUriContent { get; set; }
    public string FreeUpsellExternalBrowserSteamUrl { get; set; }
    public string FreeUpsellExternalBrowserUrl { get; set; }
    public ServerMessage LockScreenButtonText { get; set; }
    public ServerMessage LockScreenText { get; set; }
    public ServerMessage MOTDPopUpText { get; set; }
    public ServerMessage MOTDText { get; set; }
    public ServerMessage ReleaseNotesDescription { get; set; }
    public ServerMessage ReleaseNotesHeader { get; set; }
    public ServerMessage ReleaseNotesText { get; set; }
    public ServerMessage WhatsNewDescription { get; set; }
    public ServerMessage WhatsNewHeader { get; set; }
    public ServerMessage WhatsNewText { get; set; }

    public ServerMessageOverrides FillDummyData()
    {
        MOTDText = DataAccess.GetMOTD();
        MOTDPopUpText = DataAccess.GetMOTD();

        ReleaseNotesHeader = DataAccess.GetReleaseNotesHeader();
        ReleaseNotesDescription = DataAccess.GetReleaseNotesDescription();
        ReleaseNotesText = DataAccess.GetReleaseNotesText();

        WhatsNewHeader = DataAccess.GetWhatsNewHeader();
        WhatsNewDescription = DataAccess.GetWhatsNewDescription();
        WhatsNewText = DataAccess.GetWhatsNewText();

        LockScreenText = "LockScreenText";
        LockScreenButtonText = "LockScreenButtonText";

        FacebookOAuthRedirectUriContent = "FacebookOAuthRedirectUriContent";
        FreeUpsellExternalBrowserSteamUrl = "FreeUpsellExternalBrowserSteamUrl";
        FreeUpsellExternalBrowserUrl = "FreeUpsellExternalBrowserUrl";
        return this;
    }
}
