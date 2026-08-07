using Mentorly.Domain.Entities;

namespace Mentorly.Application.Abstractions.Persistence;

public interface IPeerReviewRepository
{
    Task<bool> HasReviewerAlreadyReviewedAsync(Guid submissionId, Guid reviewerStudentId, CancellationToken cancellationToken = default);

    Task<int> CountApprovalsForSubmissionAsync(Guid submissionId, CancellationToken cancellationToken = default);

    Task AddAsync(PeerReview review, CancellationToken cancellationToken = default);

    Task<PeerReview[]> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PeerReview?> GetByIdAsync(Guid peerReviewId, CancellationToken cancellationToken = default);

    Task UpdateAsync(PeerReview peerReview, CancellationToken cancellationToken = default);

    Task DeleteAsync(PeerReview peerReview, CancellationToken cancellationToken = default);
}
