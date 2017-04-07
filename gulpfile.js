var gulp = require('gulp');
var minify = require('gulp-minify');
 
gulp.task('build', function() {
  gulp.src('src/*.js')
  .pipe(gulp.dest("bin/"));
});