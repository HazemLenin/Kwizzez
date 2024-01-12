export default interface Quiz {
  id: string;
  title: string;
  score: number;
  questionsNumber: number;
  teacherId: string;
  teacherName: string;
  description: string;
  createdAt: string;
  updatedAt: string;
  took: boolean; // Student took the exam before whether he finished before it or not
  finished: boolean; // Student took the exam before and finsihed
  studentScoreId: string;
  isPublic: boolean;
}
