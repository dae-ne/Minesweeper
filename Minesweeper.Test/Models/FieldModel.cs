using Minesweeper.BusinessLogic;

namespace Minesweeper.Test.Models
{
    class FieldModel
    {
        public FieldModel(Model logicModel)
        {
            LogicModel = logicModel;
        }

        public Model LogicModel { get; private set; }

        public string Text
        {
            get
            {
                if (LogicModel.Status == FieldStatus.Covered)
                {
                    return "";
                }
                else if (LogicModel.Status == FieldStatus.Flag)
                {
                    return "F";
                }
                else if (LogicModel.Status == FieldStatus.QuestionMark)
                {
                    return "?";
                }
                else if (LogicModel.Status == FieldStatus.Uncovered)
                {
                    return LogicModel.Value switch
                    {
                        FieldValues.Mine => "M",
                        FieldValues.Empty => "",
                        _ => ((int)LogicModel.Value).ToString(),
                    };
                }

                return "ERROR";
            }
        }

        public bool IsEnabled => LogicModel.Status != FieldStatus.Uncovered;
    }
}
