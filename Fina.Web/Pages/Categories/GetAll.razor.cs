using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Categories
{
    public partial class GetAllCategoriesPage:ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;

        public List<Category> Categories { get; set; } = [];

        public string SearchTerm { get; set; } = string.Empty;

        #endregion

        #region Services
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Inject]
        public IDialogService Dialog { get; set; } = null!;
        [Inject]
        public ICategoryHandler Handler { get; set; }
        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;

            try
            {
                var request = new GetAllCategoryRequest();

                var result = await Handler.GetAllAsync(request);
                if (result.IsSuccess)
                {
                    Categories = result.Data ?? [];
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message,Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion


        public async void OnDeleteButtonClickedAsync(long id,string title)
        {
           var result = await Dialog.ShowMessageBox(
                "Atenção",
                $"Ao prosseguir a categaria {title} será excluida",
                yesText: "Excluir",
                cancelText: "Cancelar");


            if(result is true)
            {
                await OnDeleteAsync(id,title);

                StateHasChanged();
            }
        }

        public async Task OnDeleteAsync(long id,string title)
        {
            try
            {
                var request = new DeleteCategoryRequest
                {
                    Id = id,
                };

                var result = await Handler.DeleteAsync(request);

                Categories.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Categoria {title} removida com sucesso!",Severity.Info);
            }
            catch(Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

    }
}
