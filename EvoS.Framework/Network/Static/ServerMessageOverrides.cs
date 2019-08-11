using System;

// Token: 0x020009D9 RID: 2521
[Serializable]
public class ServerMessageOverrides
{
	public ServerMessage MOTDText { get; set; }
	public ServerMessage MOTDPopUpText { get; set; }
	public ServerMessage ReleaseNotesText { get; set; }
	public ServerMessage ReleaseNotesHeader { get; set; }
	public ServerMessage ReleaseNotesDescription { get; set; }
	public ServerMessage WhatsNewText { get; set; }
	public ServerMessage WhatsNewHeader { get; set; }
	public ServerMessage WhatsNewDescription { get; set; }
	public ServerMessage LockScreenText { get; set; }
	public ServerMessage LockScreenButtonText { get; set; }
	public string FreeUpsellExternalBrowserUrl { get; set; }
	public string FreeUpsellExternalBrowserSteamUrl { get; set; }
	public ServerMessage FacebookOAuthRedirectUriContent { get; set; }
}
