pipeline {
    agent any
    
    stages {
        stage('Clean Workspace') {
            steps {
                // Clean workspace
                cleanWs()
            }
        }
        stage('Checkout') {
            steps {
                // Download code from Git
                git branch: 'main', credentialsId: 'github', url: 'https://github.com/eli-is/app.git'
            }
        }
        stage('Build .NET Core Application & Build Docker Image') {
            steps {
                script {
                    // Build Docker image
                     //docker.build("eli7890/app")
                      docker.build('eli7890/book-management:${env.BUILD_NUMBER}', '${WORKSPACE}')
    
                 //  sh """
                    //   docker build -t eli7890/book-management:${env.BUILD_NUMBER} ${WORKSPACE}
                  //    docker tag eli7890/book-management:${env.BUILD_NUMBER} eli7890/book-management:latest
                 //  """
                    
                }
            }
        }
        stage('Push Docker Image to Docker Hub') {
            steps {
                script { 
                    // Push Docker image to Docker Hub
                    withCredentials([usernamePassword(credentialsId: 'dockerhub', passwordVariable: 'DOCKER_PASSWORD', usernameVariable: 'DOCKER_USER')]) {
                        sh """
                            docker login -u ${DOCKER_USER} -p ${DOCKER_PASSWORD}
                            docker push eli7890/book-management:${env.BUILD_NUMBER}
                            docker push eli7890/book-management:latest
                        """
                    }
                }
            }
        }
        stage('Apply Kubernetes Configuration to Minikube') {
            steps {
                script {
                    // Set the kubectl context to Minikube
                    sh 'kubectl config use-context minikube'

                    // Apply service
                    sh 'kubectl apply -f service-app.yaml -n app'

                    // Apply deployment
                    sh 'kubectl apply -f deployment-app.yaml -n app'
                }
            }
        }
    }
}  
