import { Component } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css'],
})
export class TestComponent {
  constructor(private formBuilder: FormBuilder) {}
  form = this.formBuilder.group({
    title: [''],
    skills: this.formBuilder.array([
      this.formBuilder.group({
        title: '',
      }),
    ]),
  });

  addSkill() {
    this.form.controls.skills.insert(
      this.form.controls.skills.length,
      this.formBuilder.group({
        title: '',
      })
    );
  }
}
