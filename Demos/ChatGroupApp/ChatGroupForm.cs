using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChatGroupApp.WorkshopConfig;

namespace ChatGroupApp
{
    public partial class ChatGroupForm : Form
    {
        private ChatRoom Chat { get; set; }
        public ChatGroupForm()
        {
            InitializeComponent();
            CreateChatGroupTopic();
        }
        private void CreateChatGroupTopic()
        {
            ChatArea = lstChat;

            Topic.CreateTopicIfNotExists(ChatGroup.Name).ConfigureAwait(false);
            Topic.Enable(ChatGroup.Name).ConfigureAwait(false);
        }

        private async void btnSignInOut_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;

            if (btnSignInOut.Text == "Sign in")
                await Login();
            else
                DisableChat();

            UseWaitCursor = false;
        }

        private async Task Login()
        {
            try
            {
                var username = txtUsername.Text;
                var chatMessageManager = new ChatMessageManager(lstChat);

                Chat = new ChatRoom(chatMessageManager, ChatGroup.Name, username);
                await Chat.Start();
                await Chat.Send($"Joined ..");

                EnableChat();
            }
            catch (ChatException exception)
            {
                MessageBox.Show(exception.Message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private async void btnSendMessage_Click(object sender, EventArgs e)
        {
            var message = txtMessage.Text.Trim();
            await Chat.Send(message);

            ClearMessageTextbox();
        }
        private void ClearMessageTextbox()
        {
            txtMessage.Invoke(ctrl => ctrl.Clear());
        }
        private void EnableChat() => ChangeChatStatus(true);
        private void DisableChat() => ChangeChatStatus(false);
        private void ChangeChatStatus(bool value)
        {
            pnlAuthArea.Invoke(ctrl => ctrl.Enabled = value);
            if (value)
            {
                btnSignInOut.Invoke(ctrl => ctrl.Text = "Sign out");
                btnBlockUsers.Invoke(ctrl => ctrl.Enabled = false);
                txtMessage.Invoke(ctrl => ctrl.Focus());
                txtUsername.Invoke(txt => txt.Enabled = false);
                this.Invoke(frm => frm.AcceptButton = btnSendMessage);
                this.Invoke(frm => frm.Text = $"Chat Group » {Chat.MyUsername}");
            }
            else
            {
                btnSignInOut.Invoke(ctrl => ctrl.Text = "Sign in");
                btnBlockUsers.Invoke(ctrl => ctrl.Enabled = false);
                txtUsername.Invoke(ctrl => ctrl.Focus());
                txtUsername.Invoke(txt => txt.Enabled = true);
                this.Invoke(ctrl => ctrl.AcceptButton = btnSignInOut);
                this.Invoke(frm => frm.Text = "Chat Group");
            }
        }

        private async void btnLoadUsers_Click(object sender, EventArgs e)
        {
            var users = await Chat.LoadUsers();
            ShowUsers(users);
        }
        private void ShowUsers(List<string> users)
        {
            lstUsers.Items.Clear();
            users.ForEach(user => lstUsers.Items.Add(user));

            if (users.Count > 0)
            {
                btnBlockUsers.Enabled = true;
                btnUnblockAll.Enabled = true;
                btnAddEncryptKey.Enabled = true;
                btnRemoveEncryptKey.Enabled = true;
            }
        }

        private async void btnBlockUsers_Click(object sender, EventArgs e)
        {
            await Chat.BlockUsers(GetSelectedUsers());
        }
        private IEnumerable<string> GetSelectedUsers()
        {
            foreach (var selectedItem in lstUsers.SelectedItems)
            {
                yield return selectedItem.ToString().ToLower();
            }
        }

        private async void btnUnblockAll_Click(object sender, EventArgs e)
        {
            await Chat.UnblockAllUsers();
        }

        private void chkEnableEncryption_CheckedChanged(object sender, EventArgs e)
        {
            ManageEncryption();
        }
        private void ManageEncryption()
        {
            if (chkEnableEncryption.Checked)
            {
                txtCipherKey.Visible = true;

                var key = txtCipherKey.Text.Trim();
                Encryption.Enable(key);
                Encryption.AddKey(Chat.MyUsername, key);
            }
            else
            {
                txtCipherKey.Visible = false;
                Encryption.Disable();
                Encryption.RemoveKey(Chat.MyUsername);
            }
        }

        private void BtnAddEncryptKey_Click(object sender, EventArgs e)
        {
            string key = Prompt.ShowDialog("Please enter key", "Decryption Key");
            if (string.IsNullOrWhiteSpace(key))
                return;

            Encryption.AddKey(GetSelectedUsers(), key);
        }

        private void BtnRemoveEncryptKey_Click(object sender, EventArgs e)
        {
            Encryption.RemoveKey(GetSelectedUsers());
        }

        private async void btnDisableSending_Click(object sender, EventArgs e)
        {
            await Topic.Disable(ChatGroup.Name);
        }
    }
}
