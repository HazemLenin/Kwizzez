import AnswerForm from './AnswerForm';

export default interface QuestionForm {
  title: String;
  image: String;
  order: number;
  degree: number;
  answers: AnswerForm[];
}
