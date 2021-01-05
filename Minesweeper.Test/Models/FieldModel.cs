using Minesweeper.BusinessLogic;

namespace Minesweeper.Test.Models
{
    // TODO: Create more properties that return values from the logic model
    class FieldModel : IModel
    {
        private readonly IModel _logicModel;

        public FieldModel(IModel logicModel)
        {
            _logicModel = logicModel;
        }

        public int Id => _logicModel.Id;

        public FieldStatus Status
        {
            get => _logicModel.Status;
            set => _logicModel.Status = value;
        }

        public FieldValues Value
        {
            get => _logicModel.Value;
            set => _logicModel.Value = value;
        }

        // TODO: Change property name to "Content". It should contain an image
        public string Text
        {
            get
            {
                if (_logicModel.Status == FieldStatus.Covered)
                {
                    return "";
                }
                else if (_logicModel.Status == FieldStatus.Flag)
                {
                    return "F";
                }
                else if (_logicModel.Status == FieldStatus.QuestionMark)
                {
                    return "?";
                }
                else if (_logicModel.Status == FieldStatus.Uncovered)
                {
                    return _logicModel.Value switch
                    {
                        FieldValues.Mine => "M",
                        FieldValues.Empty => "",
                        _ => ((int)_logicModel.Value).ToString(),
                    };
                }

                return "ERROR";
            }
        }

        public bool IsEnabled => _logicModel.Status != FieldStatus.Uncovered;
    }
}
