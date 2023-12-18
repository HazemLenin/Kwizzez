import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'numberToASCII',
})
export class NumberToASCIIPipe implements PipeTransform {
  transform(value: number, ...args: unknown[]): string {
    return String.fromCharCode(value);
  }
}
