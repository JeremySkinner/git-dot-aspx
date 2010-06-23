namespace GitAspx.Controllers {
	using System.ComponentModel;

	public enum Rpc {
		[Description("upload-pack")]
		UploadPack, 
		[Description("receive-pack")]
		ReceivePack
	}
}