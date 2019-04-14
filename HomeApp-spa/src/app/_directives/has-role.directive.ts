import { Directive, OnInit, ViewContainerRef, TemplateRef, Input } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {

  @Input() appHasRole: string[]; // requiredRoles

  constructor(private viewContainerRef: ViewContainerRef, private templateRef: TemplateRef<any>, private authService: AuthService) { }

  ngOnInit() {

    const userRoles = this.authService.decodedToken.role as string[];

    if (!userRoles) {
      this.viewContainerRef.clear();
    }

    // check is user has required roles
    if (this.authService.roleMatch(this.appHasRole)) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
   } else {
      this.viewContainerRef.clear();
   }

  }

}
