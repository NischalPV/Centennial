import { ErrorStateMatcher } from '@angular/material/core';
import { FormControl, FormGroupDirective, NgForm, Validators, FormGroup, FormBuilder } from '@angular/forms';

export class FieldsErrorMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control && control?.invalid && control.parent?.dirty);
    const invalidParent = !!(control && control.parent && control.parent?.invalid && control.parent?.dirty);

    return (invalidCtrl || invalidParent);
  }
}