using Minesweeper.BusinessLogic;

namespace Minesweeper.Test.Models
{
    class FieldModel
    {
        public FieldModel(Model logicModel, string text)
        {
            LogicModel = logicModel;
            Text = text;
        }

        public Model LogicModel { get; private set; }
        public string Text { get; set; }
        public bool IsEnabled => LogicModel.Status != FieldStatus.Uncovered;
    }
}
