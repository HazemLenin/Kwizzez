export default interface Quiz {
  id: string;
  title: string;
  score: Number;
  questionsNumber: Number;
  teacherId: string;
  teacherName: string;
  description: string;
  createdAt: string;
  updatedAt: string;
  took: Boolean; // Student took the exam before whether he finished before it or not
  finished: Boolean; // Student took the exam before and finsihed
  studentScoreId: String;
}
