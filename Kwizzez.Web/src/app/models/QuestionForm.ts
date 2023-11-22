import AnswerForm from './AnswerForm';

export default interface QuestionForm {
  title: String;
  image: String | null;
  order: number;
  degree: number;
  answers: AnswerForm[];
}
