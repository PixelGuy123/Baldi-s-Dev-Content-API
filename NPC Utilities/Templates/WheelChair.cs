namespace BaldiDevContentAPI.NPCs.Templates
{
	/// <summary>
	/// Funny Chair, you can use it as a template :)
	/// </summary>
	public  class WheelChair : NPC
	{
		private void Start()
		{
			animator = GetComponent<CustomNPC_Animator>();
			animator.AnimationSpeed = 9f;
			animator.SetAnimation("normal"); // Just a simple setup
		}



		CustomNPC_Animator animator;
	}
}
